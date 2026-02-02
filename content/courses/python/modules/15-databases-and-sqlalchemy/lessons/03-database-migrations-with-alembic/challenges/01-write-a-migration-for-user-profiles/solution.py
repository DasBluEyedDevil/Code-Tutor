"""add user_profiles table

Revision ID: profile001
Revises: users001
Create Date: 2025-01-15

"""
from alembic import op
import sqlalchemy as sa

revision = 'profile001'
down_revision = 'users001'
branch_labels = None
depends_on = None


def upgrade() -> None:
    """Create user_profiles table with FK to users."""
    op.create_table(
        'user_profiles',
        sa.Column('id', sa.Integer(), primary_key=True),
        sa.Column('user_id', sa.Integer(), nullable=False, unique=True),
        sa.Column('bio', sa.String(500), nullable=True),
        sa.Column('avatar_url', sa.String(255), nullable=True),
        sa.Column('location', sa.String(100), nullable=True),
        sa.Column('website', sa.String(255), nullable=True),
        sa.Column(
            'created_at',
            sa.DateTime(),
            server_default=sa.func.now()
        ),
        sa.Column('updated_at', sa.DateTime(), nullable=True),
        # Foreign key with cascade delete
        sa.ForeignKeyConstraint(
            ['user_id'],
            ['users.id'],
            ondelete='CASCADE'
        ),
    )
    
    # Index for faster user lookups
    op.create_index(
        'ix_user_profiles_user_id',
        'user_profiles',
        ['user_id'],
        unique=True  # Enforce one profile per user at DB level
    )


def downgrade() -> None:
    """Remove user_profiles table."""
    # Drop index first
    op.drop_index('ix_user_profiles_user_id', 'user_profiles')
    
    # Drop table (FK constraint is dropped with table)
    op.drop_table('user_profiles')


# Demonstration
if __name__ == "__main__":
    print("=== Migration: Add User Profiles ===")
    print("\nupgrade() creates:")
    print("  - user_profiles table with all columns")
    print("  - Foreign key to users with CASCADE delete")
    print("  - Unique index on user_id")
    print("\ndowngrade() removes (reverse order):")
    print("  1. Drop index")
    print("  2. Drop table (FK removed automatically)")
    print("\nRun with:")
    print("  alembic upgrade profile001")
    print("  alembic downgrade users001")