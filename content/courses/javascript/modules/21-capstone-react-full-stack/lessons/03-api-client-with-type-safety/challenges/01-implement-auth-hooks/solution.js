// packages/web/src/hooks/use-auth.ts
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient, ApiError } from '../lib/api-client';
import { RegisterInput, LoginInput, User } from '@app/shared';

export function useMe() {
  return useQuery(
    ['auth', 'me'],
    () => apiClient.getMe(),
    {
      staleTime: 10 * 60 * 1000, // 10 minutes
      retry: false,
      onError: (error: ApiError) => {
        if (error.status === 401) {
          apiClient.logout();
        }
      },
    }
  );
}

export function useLogin() {
  const queryClient = useQueryClient();

  return useMutation(
    (input: LoginInput) => apiClient.login(input),
    {
      onSuccess: (response) => {
        queryClient.setQueryData(['auth', 'me'], response.user);
      },
      onError: (error: ApiError) => {
        console.error('Login failed:', error.message);
      },
    }
  );
}

export function useRegister() {
  const queryClient = useQueryClient();

  return useMutation(
    (input: RegisterInput) => apiClient.register(input),
    {
      onSuccess: (response) => {
        queryClient.setQueryData(['auth', 'me'], response.user);
      },
      onError: (error: ApiError) => {
        console.error('Registration failed:', error.message);
      },
    }
  );
}

export function useLogout() {
  const queryClient = useQueryClient();

  return useMutation(
    () => Promise.resolve(apiClient.logout()),
    {
      onSuccess: () => {
        queryClient.removeQueries('auth');
        queryClient.removeQueries('tasks');
      },
    }
  );
}