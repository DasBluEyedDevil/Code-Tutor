FROM python:3.11-slim

WORKDIR /app

# Install dependencies
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy application code
COPY . .

# Create a non-root user for security
RUN useradd -m -u 1000 executor && \
    chown -R executor:executor /app

USER executor

EXPOSE 4000

CMD ["python", "app.py"]
