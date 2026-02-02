from sqlalchemy import create_engine, Column, Integer, String, Float, Boolean, ForeignKey, DateTime
from sqlalchemy.orm import sessionmaker, declarative_base, relationship
from datetime import datetime

Base = declarative_base()

# TODO: Define Book model
class Book(Base):
    __tablename__ = 'books'
    # Add columns: id, title, author, price, in_stock
    pass

# TODO: Define Order model
class Order(Base):
    __tablename__ = 'orders'
    # Add columns: id, book_id (FK), quantity, order_date
    pass

# Test your models
engine = create_engine('sqlite:///:memory:')
Base.metadata.create_all(engine)
Session = sessionmaker(bind=engine)
session = Session()

# Add test data and queries here