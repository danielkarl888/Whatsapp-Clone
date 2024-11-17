# WhatsApp Clone Server 📱

Welcome to the **WhatsApp Clone Server**!  
This server was designed and developed by **Daniel Meir Karl** and **Dvir Landau**. 👨‍💻👨‍💻

![image](https://github.com/user-attachments/assets/bd4cf0c9-fa89-420b-a4fb-0e462779a9d8)

---

## Project Purpose 🎯

The **WhatsApp Clone** project aims to replicate the core functionality of the popular messaging app, WhatsApp. The purpose of this project is to provide a platform for real-time messaging, where users can interact with each other, manage contacts, and send messages in real time. This clone focuses on demonstrating the integration of backend services, real-time communication via **SignalR**, and server-client interaction in a modern web application. It is a learning project built with the goal of improving our understanding of API development, real-time features, and modern web technologies.

---

## Getting Started 🚀

### Starting the Server ⚙️

To start the server, you'll need to run **both** the following projects:  
- `WAppBIU_Server`  
- `WebAPI`  

Running these projects will enable the server-side functionality and the ranking page. 🎯

---

### Client-Side Integration 🔌

To integrate the client-side (React) with the server, follow these steps:

1. Clone the repository from the following link:  
   [WAppBIU Client Repository](https://github.com/danielkarl888/WAppBIU/tree/serverBranchKarl)  

2. **Important:** Clone the branch named `serverBranchKarl`. This branch is specifically adapted to work with the server. 🔑

3. Follow the running instructions provided in the README file of the client repository. 📝

---

## SignalR Integration ⚡

The server uses **SignalR** for real-time messaging. 💬  

To set up SignalR on the client side:  
1. Navigate to the client folder.  
2. Run the following command to install the SignalR package:  
   ```bash
   npm i --save @microsoft/signalr
   ```

### Notes:  
- SignalR supports **live messaging** functionality. 🔴  
- When adding a new contact for a user, **both users must re-login** to update their chat. (This limitation is due to the project's requirements, which only mandate real-time messaging functionality.) 🔄

---

## Pre-Registered Users 🧑‍🤝‍🧑

To make testing easier, the following users are pre-registered on the server:  

| Username | Password |  
|----------|----------|  
| david    | david1   |  
| raz      | raz1     |  

These users already have some contacts and messages set up for testing. 🧪

---

## Server and Client Addresses 🌍

- **WebAPI Server Address:** `http://localhost:5030`  
  All server data can be accessed through this address. 🌐  

- **React Client Address:** `http://localhost:3000`  
  Access the client-side React app from here. 💻

---

## API Testing with Swagger 🧑‍🔬

To test the WebAPI using **Swagger**, follow these steps:  

1. Register a new user via:  
   ```  
   POST /api/Users/Register  
   ```  
2. Log in with the registered user via:  
   ```  
   POST /api/Users/Login  
   ```  
3. To test any **/api/contacts/.../** endpoints, include the `user` parameter (the logged-in user) to access their data.  

_Note: The client side already integrates with all these APIs._ 🔄  

---

Enjoy using the WhatsApp Clone Server! 🎉

---
