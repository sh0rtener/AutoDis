FROM nginx:1.27-alpine

RUN rm -f /etc/nginx/conf.d/default.conf

RUN mkdir -p /var/www/files && chown -R nginx:nginx /var/www/files

EXPOSE 80