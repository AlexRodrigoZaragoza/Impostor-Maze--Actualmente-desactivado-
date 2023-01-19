<?php

	require 'ConnectionSettings.php';

	//Variables submited by user
	$nickUser = $_POST["nick"];
	$userPassword = $_POST["password"];

	if($conn) 
	{
		//Preventing sql injections
		$statement = $conn->prepare("SELECT * FROM table_user WHERE NICK = ? AND PASSWORD = ?");
		$statement->bind_param("ss",$nickUser,$userPassword);
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
			echo "Wrong credentials";
		}
		
		$conn->close();
	}
?>