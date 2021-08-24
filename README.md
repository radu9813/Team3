# Team3

##How to create a docker container and image of the project.
```
docker build . -t team-app-3 

docker run -d -p 8081:80 --name team-app-3-container team-app-3
```

## How to deploy to heroku 
Login to heroku
```
heroku login
heroku container:login
```

Push container
```
heroku container:push -a tem-app-3 web
```

Release the container
```
heroku container:release -a team-app-3 web
```
