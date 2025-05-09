# Step 1: Build Stage
FROM node:24.0.0-alpine AS build

# Set working directory
WORKDIR /app

# Update npm to version 11.3.0
RUN npm install -g npm@11.3.0

# Install dependencies
COPY package*.json ./
RUN npm install

# Copy source code
COPY . .

# Build production React app
RUN npm run build

# Step 2: Production Stage (nginx)
FROM nginx:alpine

# Copy built React app to nginx html directory
COPY --from=build /app/build /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
