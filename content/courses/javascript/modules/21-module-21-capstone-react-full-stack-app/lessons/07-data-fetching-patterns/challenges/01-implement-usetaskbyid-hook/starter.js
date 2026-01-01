export function useTaskById(taskId: string | null) {
  return useQuery({
    queryKey: [], // Your key here
    queryFn: async () => {
      // Your fetch logic
    },
  });
}