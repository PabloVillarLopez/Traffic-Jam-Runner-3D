<?php
$servidor = "localhost";
$bbdd = "bbddlogin";
$user = "root";
$pass = "";

try {
	$conn = mysqli_connect($servidor, $user, $pass, $bbdd);

	if (!$conn) {
		echo "
		'{Codigo: 400', 'Mensaje': 'Error intentando conectar', 'Respuesta': ''
		}";
	}
	else {
		echo "
		'{Codigo: 200', 'Mensaje': 'Conectado correctamente', 'Respuesta': ''
		}";
	}
} catch (Exception $e) {
	echo "
		'{Codigo: 400', 'Mensaje': 'Error intentando conectar', 'Respuesta': ''
		}";
}


$conn->close();
?>