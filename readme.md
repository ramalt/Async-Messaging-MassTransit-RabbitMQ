# Microservice Messaging With MediatR + MassTransit 

![masstransit ](https://i0.wp.com/henriquemauri.net/wp-content/uploads/2021/09/masstransit.png?resize=810%2C500&ssl=1)

This microservices project comprises two main services: **OrderService** and **StockService**. Communication between these services is established using the Event Driven Architecture paradigm. MassTransit RabbitMQ is chosen as the event bus, and MediatR is used for handling events. The project is organized into two essential components: **Contracts** and **Components**.

## Structure

![](/Docs/schema.png)

### Contracts Class Library

- This library contains definitions for the Order events used in inter-service communication.
- Notable examples include **OrderSubmitted.cs** and **SubmitOrder.cs**, which standardize message types for seamless sharing between services.

### Components Class Library

- The Components library houses components such as **SubmitOrderConsumer.cs**, responsible for handling events. These components capture incoming events, process them, and trigger new events as a result.
- Specifically, **SubmitOrderConsumer.cs** listens for the **SubmitOrder** event and processes it to generate an **OrderSubmitted** event.




## Technologies

1. **MassTransit RabbitMQ:** MassTransit RabbitMQ is utilized as the event bus for reliable and scalable communication between microservices.

2. **MediatR:** MediatR is employed for processing event messages, providing a modular and organized approach to event handling within the application.

## Design Patterns

- **Event Driven Architecture (EDA):** Communication between services is based on the EDA pattern. Each event is listened to and processed by interested services.

## Approaches

- **Modular Approach:** Components are organized into separate class libraries, facilitating project maintenance and development.
- **Standardization :** The Contracts library standardizes events and messages, ensuring reliable communication.
- **Processing and Response:** Components like **SubmitOrderConsumer.cs** handle incoming events and trigger new events as a result, enabling services to interact effectively.

