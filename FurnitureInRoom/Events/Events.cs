using FurnitureInRoom.BusinessEntities;

namespace FurnitureInRoom.Events
{
    public delegate void FurnitureAddedEventHandler(object sender, Furniture furnitureAdded);
    public delegate void FurnitureRemovedEventHandler(object sender, Furniture furnitureRemoed, Room anotherRoom);

    public delegate void RoomAddedEventHandler(object sender, Room roomAdded);
    public delegate void RoomRemovedEventHandler(object sender, Room roomRemoved, Room anotherRoom);
}
