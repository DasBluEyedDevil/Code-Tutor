// packages/web/src/hooks/use-auth.ts
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '../lib/api-client';
import { RegisterInput, LoginInput } from '@app/shared';

// Hook for getting current user
export function useMe() {
  // Query to fetch current user
}

// Hook for user login
export function useLogin() {
  // Mutation for login
}

// Hook for user registration
export function useRegister() {
  // Mutation for registration
}

// Hook for user logout
export function useLogout() {
  // Mutation for logout
}