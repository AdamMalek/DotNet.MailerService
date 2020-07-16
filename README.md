Things that I have not included, but would be required to be considered production ready:
- distributed logging
- service registry integration
- some kind of key vault to store smtp credentials / db connection string


Things that I would do to increase daily sent mails count to few millions per day:
- Asynchronous communication
- Full CQRS - denormalized read model synchronized by events
- scale horizontally
- introduce some kind of data sharding