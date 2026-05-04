# Steps for Deployment
1. Pull the changes
2. Edit the .env file to include the JWT secret
3. Add the env variable to the docker compose file
4. Drop the chirp database in prod
5. Re-add the chirp database by applying migrations

## Next Steps
- Build a `Utilities` folder in `Core` and add a class that has helper methods for converting UTC timestamps to DateTime and vice versa.
- Add a Background service that calls the SimpleFin API every 4 hours (dates can be from last month).
- Add `Transaction`, `Holding`, `Connection` and `Account` as database tables. 
- Build onto the `DtoToModelMapper` to convert the `AccountSet` to the above tables, considering relations as well (update vs create).
- Rework the front-facing API to get the database tables and not call the SimpleFin API.