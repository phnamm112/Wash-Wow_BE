# Wash-Wow_BE
Step 1: run CLI update database

dotnet ef database update --startup-project "WashAndWow.API" --project "WashAndWow.Infrastructure"

- Nếu chưa có migration trong Infrastructure
-> chạy migrate

dotnet ef migrations add v1 --startup-project "WashAndWow.API" --project "WashAndWow.Infrastructure"

Step 2: run project ( nhớ check xem startup đã là WashAndWow.API chưa ) 
