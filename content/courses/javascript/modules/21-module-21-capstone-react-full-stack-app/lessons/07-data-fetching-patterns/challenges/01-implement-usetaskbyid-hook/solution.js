export function useTaskById(taskId: string | null) {
  return useQuery({
    queryKey: ['tasks', taskId],
    queryFn: async () => {
      const response = await apiClient.request('GET', `/api/tasks/${taskId}`);
      return response.task;
    },
    enabled: !!taskId,
    staleTime: 1000 * 60 * 5,
  });
}