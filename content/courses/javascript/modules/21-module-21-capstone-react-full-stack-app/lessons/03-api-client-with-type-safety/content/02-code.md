---
type: "EXAMPLE"
title: "Create API Client Module"
---

Build a type-safe fetch wrapper in the web package:

```typescript
// packages/web/src/lib/api-client.ts
import {
  ApiResponse,
  PaginatedResponse,
  Task,
  TaskWithCategory,
  Category,
  User,
  AuthResponse,
  CreateTaskInput,
  UpdateTaskInput,
  CreateCategoryInput,
  TaskFilters,
  API_ENDPOINTS,
} from '@app/shared';

// API error class
export class ApiError extends Error {
  constructor(
    public status: number,
    public code: string,
    message: string
  ) {
    super(message);
    this.name = 'ApiError';
  }
}

// Base fetch wrapper
class ApiClient {
  private baseUrl: string;
  private token: string | null = null;

  constructor(baseUrl: string = 'http://localhost:3000') {
    this.baseUrl = baseUrl;
    this.loadToken();
  }

  private loadToken() {
    if (typeof window !== 'undefined') {
      this.token = localStorage.getItem('auth_token');
    }
  }

  private setToken(token: string) {
    this.token = token;
    localStorage.setItem('auth_token', token);
  }

  private clearToken() {
    this.token = null;
    localStorage.removeItem('auth_token');
  }

  private async request<T>(
    method: string,
    path: string,
    options?: {
      body?: unknown;
      params?: Record<string, string | number | boolean>;
    }
  ): Promise<T> {
    const url = new URL(`${this.baseUrl}${path}`);

    // Add query parameters
    if (options?.params) {
      Object.entries(options.params).forEach(([key, value]) => {
        url.searchParams.append(key, String(value));
      });
    }

    const headers: HeadersInit = {
      'Content-Type': 'application/json',
    };

    // Add auth token if available
    if (this.token) {
      headers['Authorization'] = `Bearer ${this.token}`;
    }

    const response = await fetch(url.toString(), {
      method,
      headers,
      body: options?.body ? JSON.stringify(options.body) : undefined,
    });

    // Handle non-JSON responses
    const contentType = response.headers.get('content-type');
    const isJson = contentType?.includes('application/json');

    if (!response.ok) {
      let errorMessage = 'API error';
      let errorCode = 'UNKNOWN_ERROR';

      if (isJson) {
        const error = await response.json();
        errorMessage = error.error?.message || error.error || errorMessage;
        errorCode = error.error?.code || errorCode;
      }

      // Handle 401 - logout user
      if (response.status === 401) {
        this.clearToken();
      }

      throw new ApiError(response.status, errorCode, errorMessage);
    }

    // Handle response
    if (!isJson) {
      throw new Error('Invalid response type from API');
    }

    const data = await response.json();
    return data;
  }

  // Auth endpoints
  async register(
    input: { email: string; password: string; name?: string }
  ): Promise<AuthResponse> {
    const response = await this.request<ApiResponse<AuthResponse>>(
      'POST',
      API_ENDPOINTS.AUTH.REGISTER,
      { body: input }
    );

    if (response.data?.token) {
      this.setToken(response.data.token);
    }

    return response.data!;
  }

  async login(input: { email: string; password: string }): Promise<AuthResponse> {
    const response = await this.request<ApiResponse<AuthResponse>>(
      'POST',
      API_ENDPOINTS.AUTH.LOGIN,
      { body: input }
    );

    if (response.data?.token) {
      this.setToken(response.data.token);
    }

    return response.data!;
  }

  async getMe(): Promise<User> {
    const response = await this.request<ApiResponse<{ user: User }>>(
      'GET',
      API_ENDPOINTS.AUTH.ME
    );
    return response.data!.user;
  }

  async logout() {
    this.clearToken();
  }

  // Task endpoints
  async getTasks(
    filters?: TaskFilters
  ): Promise<PaginatedResponse<TaskWithCategory>> {
    const response = await this.request<
      ApiResponse<PaginatedResponse<TaskWithCategory>>
    >('GET', API_ENDPOINTS.TASKS.LIST, {
      params: filters as Record<string, string | number | boolean>,
    });
    return response.data!;
  }

  async getTask(id: string): Promise<TaskWithCategory> {
    const response = await this.request<ApiResponse<TaskWithCategory>>(
      'GET',
      API_ENDPOINTS.TASKS.GET(id)
    );
    return response.data!;
  }

  async createTask(input: CreateTaskInput): Promise<Task> {
    const response = await this.request<ApiResponse<Task>>(
      'POST',
      API_ENDPOINTS.TASKS.CREATE,
      { body: input }
    );
    return response.data!;
  }

  async updateTask(id: string, input: UpdateTaskInput): Promise<Task> {
    const response = await this.request<ApiResponse<Task>>(
      'PUT',
      API_ENDPOINTS.TASKS.UPDATE(id),
      { body: input }
    );
    return response.data!;
  }

  async deleteTask(id: string): Promise<{ message: string }> {
    return this.request<{ message: string }>(
      'DELETE',
      API_ENDPOINTS.TASKS.DELETE(id)
    );
  }

  // Category endpoints
  async getCategories(): Promise<Category[]> {
    const response = await this.request<ApiResponse<Category[]>>(
      'GET',
      API_ENDPOINTS.CATEGORIES.LIST
    );
    return response.data!;
  }

  async createCategory(input: CreateCategoryInput): Promise<Category> {
    const response = await this.request<ApiResponse<Category>>(
      'POST',
      API_ENDPOINTS.CATEGORIES.CREATE,
      { body: input }
    );
    return response.data!;
  }

  async updateCategory(
    id: string,
    input: Partial<CreateCategoryInput>
  ): Promise<Category> {
    const response = await this.request<ApiResponse<Category>>(
      'PUT',
      API_ENDPOINTS.CATEGORIES.UPDATE(id),
      { body: input }
    );
    return response.data!;
  }

  async deleteCategory(id: string): Promise<{ message: string }> {
    return this.request<{ message: string }>(
      'DELETE',
      API_ENDPOINTS.CATEGORIES.DELETE(id)
    );
  }
}

// Create singleton instance
export const apiClient = new ApiClient();
```
