extends TemplateCharacter

var motion = Vector2()

const SPRINTING_SPEED = SPEED * 2.5;
const MAX_SPRINTING_SPEED = MAX_SPEED * 2.5;

enum MOVEMENT_AXIS { X_AXIS = 0, Y_AXIS = 1 };

func _physics_process(delta):
	update_movement();
	return move_and_slide(motion);

func update_movement():
	
	var movement_speed = SPEED if not Input.is_action_pressed("sprint") else SPRINTING_SPEED;
	var max_movement_speed = MAX_SPEED if not Input.is_action_pressed("sprint") else MAX_SPRINTING_SPEED;
	
	look_at(get_global_mouse_position());
	if Input.is_action_pressed("move_up") and not Input.is_action_pressed("move_down"):
		move(-movement_speed, -max_movement_speed, MOVEMENT_AXIS.Y_AXIS);
	elif Input.is_action_pressed("move_down") and not Input.is_action_pressed("move_up"):
		move(movement_speed, max_movement_speed, MOVEMENT_AXIS.Y_AXIS);
	else:
		motion.y = lerp(motion.y, 0, FRICTION);
	
	if Input.is_action_pressed("move_right") and not Input.is_action_pressed("move_left"):
		move(movement_speed, max_movement_speed, MOVEMENT_AXIS.X_AXIS);
	elif Input.is_action_pressed("move_left") and not Input.is_action_pressed("move_right"):
		move(-movement_speed, -max_movement_speed, MOVEMENT_AXIS.X_AXIS);
	else:
		motion.x = lerp(motion.x, 0, FRICTION);

func move(move: float, max_move: float, axis: int):
	var max_clamp_value = max_move if move > 0 else 0;
	var min_clamp_value = 0 if move > 0 else max_move;
	
	if axis == MOVEMENT_AXIS.X_AXIS:
		motion.x = clamp(motion.x + move, min_clamp_value, max_clamp_value);
	if axis == MOVEMENT_AXIS.Y_AXIS:
		motion.y = clamp(motion.y + move, min_clamp_value, max_clamp_value);

func _input(event: InputEvent) -> void:
	if event.is_action_pressed('vision_mode_toggle'):
		get_tree().call_group("Interface", "cycle_vision_mode");
