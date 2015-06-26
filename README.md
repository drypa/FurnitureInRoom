# FurnitureInRoom
Test Project

# How to Use(Example)
```
create-room -room room1 -date 01.01.2015
create-furniture -type sofa -room room1 -date 01.01.2015
create-furniture -type chair -room room2 -date 01.01.2015
create-room -room room2 -date 02.01.2015
create-room -room room3
create-furniture -type bed -room bedroom
history
query -date 01.01.2015
move-furniture -type sofa -room room1 -to room2
query -date 01.01.2015
query
create-furniture -type sofa -room room3
remove-room -room room2 -transfer room1
history
remove-room -room room1 -transfer room3
history
```
