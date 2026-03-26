# ─── Stage 1: Build ───────────────────────────────────────────
FROM node:20-alpine AS build
WORKDIR /app

# Copy package.json và lock file
COPY package*.json ./
RUN npm install

# Copy toàn bộ mã nguồn frontend
COPY . .

# Khai báo build argument
ARG VITE_API_BASE_URL="http://localhost:8080"
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL

# Build ứng dụng
RUN npm run build

# ─── Stage 2: Serve ───────────────────────────────────────────
FROM nginx:alpine AS serve
WORKDIR /usr/share/nginx/html

# Xoá file tĩnh mặc định của nginx
RUN rm -rf ./*

# Copy từ build stage
COPY --from=build /app/dist .

# Đảm bảo React Router hoạt động qua file cấu hình Nginx (SPA fallback)
RUN echo 'server { \
    listen 80; \
    server_name localhost; \
    root /usr/share/nginx/html; \
    index index.html; \
    location / { \
        try_files $uri $uri/ /index.html; \
    } \
}' > /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
