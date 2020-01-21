Feature: ShoppingBasket
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

# FIGURE OUT WHAT TAGS ARE IN SPECFLOW
#@mytag
#Scenario: Add two numbers
#	Given I have entered 50 into the calculator
#	And I have entered 70 into the calculator
#	When I press add
#	Then the result should be 120 on the screen

Background: 
# Consider passing the interface or concrete object name as argument
Given a new basket is instantiated

## INITIALISATION
#########################################################################################################################
Scenario: A newly constructed basket has zero items
Then the basket has 0 items

Scenario: The totals of a newly constructed basket are all zero
Then all totals are 0

## QUANTITIES
#########################################################################################################################

# tableA
Scenario: Adding an item without an explicit quantity results in a quantity of 1 for the item
# CAREFUL!!! Adding with a quantity of 0 is supposed to throw an exception!
When the item 'pasta' is added without an explicit quantity
Then the basket has 1 items
And the item 'pasta' has a quantity of 1

# tableA
Scenario: After adding a single item to an empty basket, both the basket and item quantity are 1
When the item 'rice' is added with a quantity of 1
Then the basket has 1 items
And the item 'rice' has a quantity of 1

Scenario: After adding two items with different quantities to an empty basket, both the basket and item quantities are correct
When the item 'bread' is added with a quantity of 3
And the item 'flour' is added with a quantity of 5
Then the basket has 2 items
And the item 'bread' has a quantity of 3
And the item 'flour' has a quantity of 5

# table for lower, higher, same
Scenario: After adding a single item to the basket, adding the same item again will update the quantity of the previously added item
Given the item 'butter' is added with a quantity of 4
When the item 'butter' is added with a quantity of 2
Then the basket has 1 items
And the item 'butter' has a quantity of 2

# Repetition of the above?
#Scenario: After updating the quantity on an item already in a basket, both the basket and item quantities are correct

# tableA ( for 0 and a negative number)
Scenario: Adding an item with, or updating an item to a quantity of 0 or less will result in an ArgumentOutOfRangeException being thrown
When the item 'eggs' is added with a quantity of 0
Then A 'ArgumentOutOfRangeException' exception is thrown

# tableA ( for 0 and a negative number)
Scenario: Updating an item to a quantity of 0 or less will result in an ArgumentOutOfRangeException being thrown
Given the item 'cerial' is added with a quantity of 1
When the item 'cerial' is added with a quantity of -1
Then A 'ArgumentOutOfRangeException' exception is thrown

## TOTALS
#########################################################################################################################
Scenario: After adding a single item to an empty basket, both the subtotal of the item and basket should equal the items unit price
Given a new basket with the following items:
| Id | Name  | Subtotal | Tax  | TaxRules |
| 1  | pasta | £1.23    | 17.5 |          |
Then the item 'pasta' has a subtotal of '£1.23'
#And the basket has a subtotal of '50p'
# Awaiting functionality to update basket subtotal

Scenario: After adding a single item (with a NoTax rule) to an empty basket, both the tax of the item and the basket should equal 0 and both the subtotal of the item and basket should equal the items unit price
Scenario: After adding two items with different quantities (both with a NoTax rule) to an empty basket, both the tax of the item and the basket should equal 0, both the subtotal and the total of the item and basket should equal the items unit price







