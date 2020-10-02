extends Node

onready var CooldownTimer: Timer = $CooldownTimer;
onready var Modulate: CanvasModulate = $Modulate;

const DARK = Color("111111");
const NIGHT_VISION = Color("37bf62");

var is_night_vision_on: bool;

func _ready() -> void:
	Modulate.color = DARK;
	is_night_vision_on = false;

func cycle_vision_mode():
	if not CooldownTimer.is_stopped():
		return;
	
	if not is_night_vision_on:
		set_night_vision_on();
	else:
		set_night_vision_off();

func set_night_vision_off():
	Modulate.color = DARK;
	$AudioStreamPlayer.stream = load("res://SFX/nightvision_off.wav")
	$AudioStreamPlayer.play();
	CooldownTimer.start();
	is_night_vision_on = false;

func set_night_vision_on():
	Modulate.color = NIGHT_VISION;
	$AudioStreamPlayer.stream = load("res://SFX/nightvision.wav")
	$AudioStreamPlayer.play();
	is_night_vision_on = true;
