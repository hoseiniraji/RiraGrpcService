# Rira gRPC Server-Client Application

This solution consists of two separate projects, both set as startup projects:

- **GrpcRiraServer**: A gRPC server with no user interface.
- **GrpcRiraClient**: A Web API project that uses Swagger as the default UI.

All CRUD operations are initiated by the client application and sent to the server using gRPC and the Protocol Buffers (Protobuf) protocol.

---

## Error Handling Strategy

If something goes wrong during communication between the client and server, there are two main strategies to handle errors:

1. **Logging and User Feedback**  
   Log the error for debugging purposes and return a meaningful error message to the client.

2. **Retry with Timeout**  
   Attempt to resend the request within a defined TTL (e.g., 30 seconds).  
   If the error persists after retries, abort the operation and record the failure in the logs.

---

## Technologies Used

- **.NET Core**
- **gRPC**
- **Protobuf**
- **Swagger (for API UI)**

---

## Getting Started

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Run the solution.

You should see the Swagger UI for the client and a running gRPC server in the background.

