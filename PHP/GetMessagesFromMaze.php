<?php

	require 'ConnectionSettings.php';

	//Variables submited by user
	$idMaze = $_POST["idMaze"];

	if($conn){
		//Preventing sql injections
		$statement = $conn->prepare("SELECT ID,MESSAGE,USER,POSITION FROM table_message WHERE MAZE = ?");

		$statement->bind_param("i",$idMaze);
		$statement->execute();

		$result = $statement->get_result();

		$statement->close();
		
		if($result->num_rows > 0){
			$data = array();
			while($obj = $result->fetch_object()){
				$data[]=$obj;
			}
			echo json_encode($data);
		}else{
			echo " There isn't messages";
		}
		
		$conn->close();
	}
?>