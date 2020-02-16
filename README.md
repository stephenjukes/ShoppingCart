## Overview
As instructed, the shopping basket provides functionality to add, update and remove items, and calculates a sub total, tax and grand total for the basket and each of its items. Whenever a basket item is updated, both the basket and the item provides updates to interested parties.

## Notifications

While not required, notifications are set up to be sent:
- in a variety of ways, (eg. email and text) 
- to a variety of users (eg. customers and retailers)

Because the shopping event args takes an `ITotals` entity,  as opposed to a basket and item entity, the content of notification messages is limited, specifically being unable to notify about the item name and quantity. While it _was_ possible to include a separate event for the basket and item, or include both the basket and item in the existing event args, my interpretation of the boiler plate setup led me in the direction of the current event args signature.

Files are created to simulate the sending of notifications, as it was originally thought that the existence of the file and its contents would provide the results necessary for testing. Subsequently, it was determined that testing according to file content resulted in tight coupling between test and production code, and that testing against a database would be more appropriate. Due to time constraints, this database is for now represented as a list of notification summaries and does not conform to the repository pattern.

Test notifications resulting from the setup in `Main` can be viewed in the console. 

### Bridge Pattern
 The bridge pattern was used in order to allow both the user type and method of communication to vary independently. The implementation of this pattern has met with some succhess, though flexibility is limited where user notifications are constrained to a finite number of paragraphs, and notification systems are free only to select which of these paragraphs to include. There is the possibility that the `UserNotification` containing a list of paragraphs might provide more flexibility. In addition to this, it would be interesting to know how each of these interfaces could be given more freedom to vary.

### Builder Pattern
User notifications are constructed from parameters that are available in two separate places. User format is defind by a set of Funcs that return a paragraph. The parameters for these Funcs then become available upon the firing of an update event, whereupon the user notification can be built.

## Considerations and Extensions

- Should user subscriptions also subscribe to a notification event, in the same way that notification systems subscribe to basket and item events?
- Is there a need for concrete baskets to be paired with concrete items? If so, consider using the abstract factory pattern 
- Add functionality to add / update in accordance with stock limits
- Notify as to whether the item was updated, in addition to being added or removed

## Improvements

There are a number of unsatisfactory issues, which would be addressed given more time.

- Implementation of a service collection or factory method pattern 
    - to prevent the basket from explicitly instantiating new items
- Division of the single feature into logical groups, as marked by its section headings
    - This was attempted but tests began to fail, probably because of features running in parallel. Further investigation required
- The architecture required for the notifications took longer than expected, time which would probably have been better spent on more extensive:
	- test coverage, (assisted by scenario outlines)
	- error handling

## Time
**70 hours**
- 2.5 ( x 16 hour ) weekends
- 15 ( x 2 hours ) work day evenings
