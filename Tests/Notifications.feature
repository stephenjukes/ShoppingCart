Feature: Notifications

Background: 
# Consider passing the interface or concrete object name as argument
Given the basket database is empty
And there are no user subscribers
And a new basket is instantiated
And the following tax rules:
| Id | RuleName                |
| 1  | NoTax                   |
| 2  | Tax20Percent            |
| 3  | Administration5poundTax |
| 4  | BandedTax2020           |
And the following notification systems
| Id | NotificationSystemName | CommunicationType | CommunicationChannel |
| 1  | EmailNotification      | email             | ..\..\..\..Emails       |
| 2  | TextNotification       | text              | ..\..\..\..Phones       |
And the following users
| Id | UserType | Email                | PhoneNumber |
| 1  | Customer | albert@hotmail.co.uk | 23855638    |
| 2  | Retailer | belle@yahoo.co.uk    | 86120183    |
| 3  | Customer | charlie@gmail.co.uk  | 41843576    |
| 4  | Retailer | diana@outlook.co.uk  | 03110894    |
| 5  | Customer | edward@aol.co.uk     | 84315325    |
And all notification systems subscribe to the basket

Scenario: Email notifications are sent correctly when items are added and removed
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 1      | email              |
And the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta |           |          |          |
And items '1' are removed
Then only the following notifications are received
| UserId | UserType | CommunicationType | Publisher | UpdateType |
| 1      | Customer | email             | basket    | add        |
| 1      | Customer | email             | item      | add        |
| 1      | Customer | email             | basket    | remove     |
| 1      | Customer | email             | item      | remove     |

Scenario: Text notifications are sent correctly when items are added and removed
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 2      | text               |
And the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta |           |          |          |
And items '1' are removed
Then only the following notifications are received
| UserId | UserType | CommunicationType | Publisher | UpdateType |
| 2      | Retailer | text              | basket    | add        |
| 2      | Retailer | text              | item      | add        |
| 2      | Retailer | text              | basket    | remove     |
| 2      | Retailer | text              | item      | remove     |

# Use the following in scenario outlines ???
Scenario: No notifications are sent on the attempted removal of a non existant item
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 3      | email, text        |
And items '1' are removed
#Then A 'ArgumentException' exception is thrown with message 'No item found with id 1'
Then no notifications are received.

Scenario: No notifications are sent on the attempted addition using an invalid quantity
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 1      | email              |
And the following items are added:
| Id | Name   | UnitPrice | Quantity | TaxRules |
| 1  | eggs   |           | 0        |          |
| 2  | cerial |           | -1       |          |
Then no notifications are received.

# How about updated?
Scenario: All notifications are sent correctly for a variety of users and subscriptions
When the users subscribe as follows for the communication types:
| UserId | CommunicationTypes |
| 1      | email, text        |
| 2      | email, text        |
| 3      | email              |
| 4      | text               |
| 5      |                    |
#Then only the users '1, 2, 3' are on the 'email' mailing list
#And only the users '1, 2, 4' are on the 'text' mailing list
When the following items are added:
| Id | Name  | UnitPrice | Quantity | TaxRules |
| 1  | pasta |           |          |          |
Then only the following notifications are received
| UserId | UserType | CommunicationType | Publisher | UpdateType |
| 1      | Customer | email             | basket    | add        |
| 1      | Customer | email             | item      | add        |
| 2      | Retailer | email             | basket    | add        |
| 2      | Retailer | email             | item      | add        |
| 3      | Customer | email             | basket    | add        |
| 3      | Customer | email             | basket    | add        |
| 1      | Customer | text              | basket    | add        |
| 1      | Customer | text              | item      | add        |
| 2      | Retailer | text              | basket    | add        |
| 2      | Retailer | text              | item      | add        |
| 4      | Retailer | text              | basket    | add        |
| 4      | Retailer | text              | basket    | add        |
