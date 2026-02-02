import { useEffect, useCallback, useRef, useState } from 'react';
import { useAuth } from '../contexts/AuthContext';

interface DecodedToken {
  exp: number;
  iat: number;
  sub: string;
}

const decodeToken = (token: string): DecodedToken | null => {
  try {
    const parts = token.split('.');
    if (parts.length !== 3) return null;
    
    const payload = parts[1];
    const decoded = atob(payload.replace(/-/g, '+').replace(/_/g, '/'));
    return JSON.parse(decoded) as DecodedToken;
  } catch {
    return null;
  }
};

const isTokenExpiringSoon = (
  token: string,
  thresholdMinutes: number = 5
): boolean => {
  const decoded = decodeToken(token);
  if (!decoded) return true;
  
  const expirationTime = decoded.exp * 1000;
  const currentTime = Date.now();
  const thresholdMs = thresholdMinutes * 60 * 1000;
  
  return expirationTime - currentTime < thresholdMs;
};

interface UseTokenRefreshOptions {
  refreshThresholdMinutes?: number;
  checkIntervalMs?: number;
}

export const useTokenRefresh = (options: UseTokenRefreshOptions = {}) => {
  const { token, logout } = useAuth();
  const { refreshThresholdMinutes = 5, checkIntervalMs = 60000 } = options;
  const isRefreshing = useRef<boolean>(false);
  const [lastRefresh, setLastRefresh] = useState<Date | null>(null);

  const refreshToken = useCallback(async () => {
    if (isRefreshing.current || !token) return;
    
    isRefreshing.current = true;
    
    try {
      const response = await fetch('/api/auth/refresh', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        }
      });
      
      if (!response.ok) {
        throw new Error('Refresh failed');
      }
      
      const data = await response.json();
      const newToken = data.token;
      
      localStorage.setItem('token', newToken);
      setLastRefresh(new Date());
      
      window.dispatchEvent(new CustomEvent('tokenRefreshed', {
        detail: { token: newToken }
      }));
      
    } catch (error) {
      console.error('Token refresh failed:', error);
      logout();
    } finally {
      isRefreshing.current = false;
    }
  }, [token, logout]);

  useEffect(() => {
    if (!token) return;
    
    const checkAndRefresh = () => {
      if (isTokenExpiringSoon(token, refreshThresholdMinutes)) {
        refreshToken();
      }
    };
    
    checkAndRefresh();
    
    const intervalId = setInterval(checkAndRefresh, checkIntervalMs);
    
    return () => clearInterval(intervalId);
  }, [token, refreshToken, refreshThresholdMinutes, checkIntervalMs]);

  return {
    refreshToken,
    isRefreshing: isRefreshing.current,
    lastRefresh
  };
};