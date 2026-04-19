# Steps for Deployment
1. Pull the changes
2. Edit the .env file to include the JWT secret
3. Add the env variable to the docker compose file
4. Drop the chirp database in prod
5. Re-add the chirp database by applying migrations