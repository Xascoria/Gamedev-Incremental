tool
extends EditorScript


func _run():
	var line1 :RichTextLabel= get_editor_interface().get_selection().get_selected_nodes()[0]
	
	var code_panel = line1.get_parent()
	
	for i in range(1,33):
		var new_inst :RichTextLabel= line1.duplicate()
		code_panel.add_child(new_inst)
		new_inst.rect_position = line1.rect_position + Vector2(0,19*i)
		new_inst.owner = get_editor_interface().get_edited_scene_root()
		new_inst.name = "Line" + str(i+1)
		
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
