// Add to src/routes/auth.ts

const changePasswordSchema = z.object({
  currentPassword: z.string().min(1, 'Current password required'),
  newPassword: z
    .string()
    .min(8, 'Password must be at least 8 characters')
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)/,
      'Password must contain uppercase, lowercase, and number'
    ),
});

// PUT /api/auth/password
auth.put(
  '/password',
  authMiddleware,
  zValidator('json', changePasswordSchema),
  async (c) => {
    // Your implementation here
  }
);