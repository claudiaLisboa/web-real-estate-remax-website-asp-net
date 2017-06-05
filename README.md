# web-real-estate-remax-website-asp-net


Website for the real estate broker REMAX that allow visitors to find houses and agents according to a group of predefined criterias.


The User Functionality Requirements

These are the functionalities of the Remax Website :

•	The visitor can search for houses based on a group of criterias : Type, Location, Prices, Number of Rooms, ….

•	 The visitor can search for agents based on a group of criteria : Agent’s number, Gender, City, Language spoken.

•	The visitor can send a message to the found Agent (via the House or Agent search).



The Analysis Functionality Requirements

This program is a multi-tiers application (Gui, Business and Datasource Layers).

- GUI Layer :
WebSite application C# with webforms, html, CSS based on the user functionality requirements.

- BUSINESS Layer :
Library of classes for the entities : Company, Employees(User, Admin or Agent), Clients(Buyer or Seller) and Houses.

- DATASOURCE Layer :
Class that encapsulates the database (access) and provides public interfaces for feeding data to the business layer and writing data back to the database.
Microsoft Access database that will contains all the needed data for Remax.


The Technical Functionality Requirements

•	For the GUI Layer : Friendly and thematic (Remax related colors and images) webform interfaces.
•	For the Datasource Layer : Ado.net.

