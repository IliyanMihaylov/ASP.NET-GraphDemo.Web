# ASP.NET-GraphDemo.Web
Application using Google plus, Facebook, Google Drive and Neo4j api.

#Getting Started
      1 - In Web.config change GraphDemoDatabase connection string.
      2 - Start Neo4j database and configure connection settings in Properties->Settings.
      3 - Now you are ready to start the project.
</br>
#Sign in
Upon login with social network, your connection details are stored in the database. In that way you will be logged in automatically. </br>
To proceed you must have a google drive account.</br>
</br>
#Google Drive
In Google Drive you must have a file with extension ".cypher" in specific file format: </br>
###### File Format - Query.cypher
MERGE (Sofia: Location { Name: 'Sofia' }) </br>
MERGE (Varna: Location { Name: 'Varna' }) </br>
MERGE (Gabrovo: Location { Name: 'Gabrovo' })</br>
MERGE (Sevlievo: Location { Name: 'Sevlievo' })</br>
MERGE (Tarnovo: Location { Name: 'Tarnovo' })</br>
MERGE (Plovdiv: Location { Name: 'Plovdiv' })</br>
MERGE (StaraZagora: Location { Name: 'StaraZagora' })</br>
</br>
MERGE (Sofia)<-[:CONNECTED_TO]->(Sevlievo)</br>
MERGE (Sevlievo)<-[:CONNECTED_TO]->(Tarnovo)</br>
MERGE (Tarnovo)<-[:CONNECTED_TO]->(Gabrovo)
MERGE (Gabrovo)<-[:CONNECTED_TO]->(Varna)</br>
MERGE (Sofia)<-[:CONNECTED_TO]->(Plovdiv)</br>
MERGE (StaraZagora)<-[:CONNECTED_TO]->(Varna)</br>
