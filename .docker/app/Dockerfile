# Fase 1: Build do Angular
FROM node:22.14 AS build

WORKDIR /app

COPY ./app/ . 

RUN npm install

RUN npm run build 

FROM nginx:alpine

COPY --from=build /app/dist/app/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
