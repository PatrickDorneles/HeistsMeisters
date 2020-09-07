extends "res://Characters/TemplateCharacter.gd"

const FOV_TOLERANCE = 20;
const RED = Color(1,0.25,0.25);
const WHITE = Color(1,1,1);

var Player;

func _ready() -> void:
	Player = get_node("/root").find_node("Player");
	

func _process(delta: float) -> void:
	if Player_in_FOV():
		pass;

func Player_in_FOV() -> bool:
	return false;
