/** @jsxImportSource @emotion/react */

import { Link } from "react-router-dom";
import { AppointmentData } from "../../Api/AppointmentsData";
import {
  appointmentAdditionalInf,
  appointmentContainer,
  appointmentTitle,
} from "../../Styles/Appointments/AppointmentStyles";
import { GiCheckMark } from "react-icons/gi";

interface Props {
  appointment?: AppointmentData | null;
}

export const Appointment = ({ appointment }: Props) => (
  <div css={appointmentContainer}>
    <Link to={`/appointment/${appointment?.id}`} css={appointmentTitle}>
      <div css={appointmentTitle}>
        {appointment?.completed && <GiCheckMark color="green" />}{" "}
        {appointment !== undefined &&
        appointment !== null &&
        appointment.description.length <= 15
          ? appointment?.description
          : appointment?.description.substring(0, 15) + "..."}
      </div>
      <div css={appointmentAdditionalInf}>
        <div>
          Доктор: {appointment?.doctorFirstName} {appointment?.doctorLastName}
        </div>
        <div>
          Пациент: {appointment?.patientFirstName}{" "}
          {appointment?.patientLastName}
        </div>
        <div>Дата: {appointment?.startDate.toString().substring(0, 10)}</div>
        <div>Время: {appointment?.startTime}</div>
      </div>
    </Link>
  </div>
);
