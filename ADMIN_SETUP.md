# Default Admin User Setup

## Overview
The Viseralbug application automatically creates a default admin user during startup if it doesn't already exist.

## Admin Credentials
- **Username**: admin@gmail.com
- **Password**: Admin@123
- **Role**: Admin
- **Name**: System Administrator

## How It Works

### Automatic Creation
The admin user is created automatically when the application starts for the first time. This happens in the `Program.cs` file during the database initialization phase.

### Code Location
The admin user creation logic is located in:
- `Program.cs` - `SeedDefaultAdminUser()` method
- `Services/PasswordHasher.cs` - Password hashing utility

### Security Features
1. **Password Hashing**: All passwords are hashed using SHA256 before storage
2. **Duplicate Prevention**: The system checks if the admin user already exists before creating
3. **Logging**: All admin user operations are logged for security tracking

## Usage

### First Time Setup
1. Start the application: `dotnet run`
2. The admin user will be automatically created
3. Check the application logs for confirmation:
   ```
   Default admin user created successfully
   Admin credentials - Username: admin@gmail.com, Password: Admin@123
   ```

### Login
1. Navigate to the login page
2. Use the admin credentials:
   - Username: admin@gmail.com
   - Password: Admin@123
3. You will receive a JWT token for API access

### API Access
With the admin role, you can access all protected endpoints:
- User management
- Project management
- Bug management
- Task management
- File uploads

## Security Considerations

### Password Policy
- The default password follows a strong pattern: `Admin@123`
- Contains uppercase, lowercase, numbers, and special characters
- **Important**: Change the default password after first login

### Role-Based Access
The admin user has full access to:
- Create, read, update, delete all entities
- Manage user accounts
- Access system settings
- View all logs and reports

### Recommendations
1. **Change Default Password**: Immediately change the admin password after first login
2. **Create Additional Admins**: Create additional admin accounts for redundancy
3. **Monitor Logs**: Regularly check application logs for admin user activities
4. **Regular Audits**: Periodically audit admin user access and activities

## Troubleshooting

### Admin User Not Created
If the admin user is not created automatically:
1. Check application logs for errors
2. Verify database connection
3. Ensure the application has write permissions to the database
4. Restart the application

### Login Issues
If you cannot login with admin credentials:
1. Verify the username and password are correct
2. Check if the user exists in the database
3. Verify the password hashing is working correctly
4. Check application logs for authentication errors

### Database Issues
If there are database-related issues:
1. Ensure SQL Server LocalDB is running
2. Check connection string in `appsettings.json`
3. Verify database permissions
4. Try recreating the database: `dotnet ef database drop` then `dotnet ef database update`

## API Endpoints for Admin

### Authentication
- `POST /api/auth/login` - Login with admin credentials

### User Management
- `GET /api/user` - Get all users (Admin only)
- `POST /api/user` - Create new user (Admin only)
- `PUT /api/user/{id}` - Update user (Admin only)
- `DELETE /api/user/{id}` - Delete user (Admin only)

### Project Management
- `GET /api/project` - Get all projects
- `POST /api/project` - Create new project (Admin only)
- `PUT /api/project/{id}` - Update project (Admin only)
- `DELETE /api/project/{id}` - Delete project (Admin only)

### Bug Management
- `GET /api/bug` - Get all bugs
- `POST /api/bug` - Create new bug
- `PUT /api/bug/{id}` - Update bug
- `DELETE /api/bug/{id}` - Delete bug (Admin only)

### Task Management
- `GET /api/task` - Get all tasks
- `POST /api/task` - Create new task
- `PUT /api/task/{id}` - Update task
- `DELETE /api/task/{id}` - Delete task (Admin only)

### File Upload
- `POST /api/fileupload/upload` - Upload general files
- `POST /api/fileupload/upload-profile-image` - Upload profile images
- `DELETE /api/fileupload/{fileName}` - Delete files (Admin only)
