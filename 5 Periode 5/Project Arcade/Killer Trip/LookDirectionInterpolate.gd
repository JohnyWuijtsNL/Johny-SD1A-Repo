extends Position2D
var interpolation_level = 25

func _process(delta):
	position = position.linear_interpolate(get_parent().get_node("LookDirection").position, interpolation_level * delta)
