FROM node:20-alpine

WORKDIR /app

# Install dependencies
COPY package.json .
RUN npm install --production

# Copy application code
COPY . .

# Create a non-root user for security
RUN adduser -D -u 1000 executor && \
    chown -R executor:executor /app

USER executor

EXPOSE 4005

CMD ["node", "server.js"]
