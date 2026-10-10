
FROM node:20-alpine AS build

WORKDIR /app

COPY src/client-side/AutoDisCms/package*.json ./
RUN npm ci

COPY src/client-side/AutoDisCms/ ./
RUN npm run build

FROM nginx:alpine AS runtime

COPY --from=build /app/dist/ /usr/share/nginx/html/

EXPOSE 8080

CMD ["nginx", "-g", "daemon off;"]