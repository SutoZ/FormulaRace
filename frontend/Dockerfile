# ---- Stage 1: Build the Angular application ----
# Use an official Node.js runtime as a parent image
FROM node:20-alpine AS builder

# Add curl for health checks
RUN apk add --no-cache curl

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json to leverage Docker cache
COPY frontend/package.json frontend/package-lock.json ./

# Install project dependencies
# Use 'npm ci' for clean, reproducible production installs
RUN npm ci

# Copy the rest of the application source code from frontend directory
COPY frontend/ .

# Build the application for production
# The output will be in /app/dist/racefrontend/browser
RUN npm run build -- --configuration production


# ---- Stage 2: Serve the application with Nginx ----
# Use a lightweight Nginx image
FROM nginx:alpine

# Install curl for health checks and debugging (required by docker-compose.yaml)
RUN apk add --no-cache curl

# Copy the built artifacts from the 'builder' stage
# Angular CLI with application builder outputs to dist/racefrontend/browser/
COPY --from=builder /app/dist/racefrontend/browser/ /usr/share/nginx/html

# Copy the custom Nginx configuration
COPY --from=builder /app/nginx.conf /etc/nginx/conf.d/default.conf

USER appuser

# Expose port 80 to the outside world
EXPOSE 80