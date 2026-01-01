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

auth.put(
  '/password',
  authMiddleware,
  zValidator('json', changePasswordSchema),
  async (c) => {
    const userId = c.get('userId');
    const { currentPassword, newPassword } = c.req.valid('json');

    // Get user with password hash
    const user = await prisma.user.findUnique({
      where: { id: userId },
    });

    if (!user) {
      return c.json({ error: 'User not found' }, 404);
    }

    // Verify current password
    const validPassword = await Bun.password.verify(
      currentPassword,
      user.passwordHash
    );

    if (!validPassword) {
      return c.json({ error: 'Current password is incorrect' }, 401);
    }

    // Hash new password
    const newHash = await Bun.password.hash(newPassword, {
      algorithm: 'argon2id',
    });

    // Update password
    await prisma.user.update({
      where: { id: userId },
      data: { passwordHash: newHash },
    });

    return c.json({ message: 'Password updated successfully' });
  }
);