/** @jsxImportSource @emotion/react */

import { Link } from "react-router-dom";
import { AppointmentData } from "../../Api/AppointmentsData";
import {
  appointmentAdditionalInf,
  appointmentContainer,
  appointmentTitle,
} from "../../Styles/Appointments/AppointmentStyles";

interface Props {
  appointment: AppointmentData;
}

export const Appointment = ({ appointment }: Props) => (
  <div css={appointmentContainer}>
    <Link to={`/appointment/${appointment.id}`} css={appointmentTitle}>
      <div css={appointmentTitle}>
        {appointment.description.length <= 15
          ? appointment.description
          : appointment.description.substring(0, 15) + "..."}
      </div>
      <div css={appointmentAdditionalInf}>
        <div>
          Доктор: {appointment.doctorFirstName} {appointment.doctorLastName}
        </div>
        <div>
          Пациент: {appointment.patientFirstName} {appointment.patientLastName}
        </div>
        <div>
          Дата: {appointment.startDate.toString().substring(0, 10)}{" "}
          {appointment.startDate.toString().substring(12, 16)}
        </div>
      </div>
    </Link>
  </div>
);
