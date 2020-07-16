Things that I have not included, but I would like to include to consider it better:
- distributed logging
- service registry integration
- some kind of key vault to store smtp credentials / db connection string
- db transaction at command handler level
- integration, e2e tests
- optimistic concurrency by entity versioning
- rest request validation aside from domain checks
- better implementations of IEmailSender and IEmailrepository
- pagination for all/pending messages list

Things that I would do to increase daily sent mails count to few millions per day:
- Asynchronous communication + horizontal scaling
- Full CQRS - denormalized read model synchronized by domain events

To start provide credentials in `appsettings.json` and run command: `docker-compose up`