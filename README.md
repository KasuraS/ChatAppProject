# ChatAppProject

A multi-threading client-server chat application using C#

## 0 - Pre-requisites

Visual Studio 2019 or any compatible version

## 1 - How to launch the app

- Open Project Solution file
- Go to Solution Explorer and right-click on the solution at the head of the list
- Click on Properties
- Go to Common Properties and select multiple startup projects
- Enable Start action for Server and ClientGUI projects (make sure to order Server first, then ClientGUI)
- Validate the update
- Select "Debug" for Solution Configurations, "Any CPU" for Solution Platforms
- Select "< Multiple Startup Projects >" for Startup Projects, and launch Start
- Wait a moment for the server to establish client connections and you can then perform actions on the client
- To create a new client, right-click on ClientGUI, select "Debug" and click on "Start a new session"

## 2 - Core functionalities

### Register

- Create a user by providing a username and a password
- Store credentials in a file

### Login

- Get access to home and use the functionalities below
- Verify credentials with those in the stored file

### Create Room

- Name and create a room
- Verify the availability of the room in the stored file
- List the owner's rooms in the first list
- List other rooms in the second list

### Join Room

- Select a room to enter in
- Display the chat log and the list of connected users

### Leave Room

- Select the same room or another one to leave the current room
- Remove the leaving user from the list
- Go back to home

### Delete Room

- Right-click on one owned room for deletion
- Delete all messages related to the deleted room
- Kick out all users connected to the deleted room

### Chat with multiple users

- Send messages between clients
- Save messages to the associated topic stored file
