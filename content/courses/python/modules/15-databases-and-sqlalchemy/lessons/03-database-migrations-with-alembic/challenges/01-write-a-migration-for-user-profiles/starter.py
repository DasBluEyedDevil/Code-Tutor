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
    # TODO: Create the user_profiles table
    # - id: Integer primary key
    # - user_id: Integer FK to users.id (unique, not null)
    # - bio: String(500), nullable
    # - avatar_url: String(255), nullable  
    # - location: String(100), nullable
    # - website: String(255), nullable
    # - created_at: DateTime with server_default
    # - updated_at: DateTime, nullable
    
    # TODO: Add foreign key with CASCADE delete
    
    # TODO: Create index on user_id
    pass


def downgrade() -> None:
    """Remove user_profiles table."""
    # TODO: Drop in reverse order of creation
    # 1. Drop index
    # 2. Drop table (FK is dropped with table)
    pass