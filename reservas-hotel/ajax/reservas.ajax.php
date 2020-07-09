<?php

require_once "../controladores/reservas.controlador.php";
require_once "../modelos/reservas.modelo.php";


class AjaxReservas{

	/*=============================================
	Traer Reserva Habitación
	=============================================*/

	public $idHabitacion;

	public function ajaxTraerReserva(){

		$valor = $this->idHabitacion;

		$respuesta = ControladorReservas::ctrMostrarReservas($valor);

		echo json_encode($respuesta);

	}

	/*=============================================
	Traer Reserva a través de Código
	=============================================*/

	public $codigoReserva;

	public function ajaxTraerCodigoReserva(){

		$valor = $this->codigoReserva;

		$respuesta = ControladorReservas::ctrMostrarCodigoReserva($valor);

		echo json_encode($respuesta);

	}

}

/*=============================================
Traer Reserva Habitación
=============================================*/

if(isset($_POST["idHabitacion"])){

	$idHabitacion = new AjaxReservas();
	$idHabitacion -> idHabitacion = $_POST["idHabitacion"];
	$idHabitacion -> ajaxTraerReserva();

}

/*=============================================
Traer Reserva Codigo
=============================================*/

if(isset($_POST["codigoReserva"])){

	$codigoReserva = new AjaxReservas();
	$codigoReserva -> codigoReserva = $_POST["codigoReserva"];
	$codigoReserva -> ajaxTraerCodigoReserva();

}
