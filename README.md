# ASP.NET-GraphDemo.Web
Application using Google plus, Facebook, Google Drive and Neo4j api.

#Getting Started
      1 - In Web.confg change GraphDemoDatabase connection string. </br>
      2 - Start Neo4j database and configurate connection settings in Properties->Settings. </br>
      3 - Now you are ready to start the project.</br>
</br>
#Sign in
Upon entry through social network in the database are recorded your details and each time you will be entered automatically. </br>
To proceed you must have a google drive account.</br>
</br>
#Google Drive
In Google Drive you must have a file with extension ".cypher" in specific file format: </br>
###### File Format - Query.cypher
MERGE (Sofia: Location { Name: 'Sofia' }) 
MERGE (Varna: Location { Name: 'Varna' }) 
MERGE (Gabrovo: Location { Name: 'Gabrovo' })
MERGE (Sevlievo: Location { Name: 'Sevlievo' })
MERGE (Tarnovo: Location { Name: 'Tarnovo' })
MERGE (Plovdiv: Location { Name: 'Plovdiv' })
MERGE (StaraZagora: Location { Name: 'StaraZagora' })

MERGE (Sofia)<-[:CONNECTED_TO { distance: 1 }]->(Sevlievo)
MERGE (Sevlievo)<-[:CONNECTED_TO { distance: 1 }]->(Tarnovo)
MERGE (Tarnovo)<-[:CONNECTED_TO { distance: 1 }]->(Gabrovo)
MERGE (Gabrovo)<-[:CONNECTED_TO { distance: 1 }]->(Varna)
MERGE (Sofia)<-[:CONNECTED_TO { distance: 1 }]->(Plovdiv)
MERGE (Plovdiv)<-[:CONNECTED_TO { distance: 1 }]->(StaraZagora)
MERGE (StaraZagora)<-[:CONNECTED_TO { distance: 1 }]->(Varna)
