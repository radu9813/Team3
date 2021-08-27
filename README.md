## How to run / deploy
### Locally (via Docker)

1. Build the container image
```bash
docker build -t p33-team3-app .
```
2. Start the app container
```bash
docker run -e DATABASE_URL={database_url} -dp {external_port}:80 p33-team3-app
```
Replace ```database_url``` with a Postgres Database URI (starting with `postgres://`).  
Replace ```external_port``` with the desired port on the host machine.

### On Heroku
Ensure you have the Heroku CLI **installed** and **authenticated**.

1. Log into the Heroku Container Registry (you only need to run this once)
```bash
heroku login
heroku container:login
```

2. Create an app in Heroku (skip this if you already have created an app)
```bash
heroku create
```

3. Build the container image and push it to the Heroku Registry
```bash
heroku container:push -a p33-team3-app web
```

4. Provision a Postgres database and add required Environment Variables
```bash
heroku addons:create heroku-postgresql:hobby-dev
heroku config:set ASPNETCORE_ENVIRONMENT=Production
```

5. Release the image to the app
```bash
heroku container:release -a p33-team3-app web
```
