Run the API 
`dotnet watch --no-hot-reload`

Flags when create a EntityFramewokr Migration
`dotnet ef migrations add InitialCreate -p Infrastructure -s API -o Data/Migrations`

If you need drop the database
`dotnet ef database drop -p Infrastructure -s API`

If you neeed remove migrations 
`dotnet ef migrations remove -p Infrastructure -s API`