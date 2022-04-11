/** @jsxImportSource @emotion/react */
import { AppointmentData } from "../../Api/AppointmentsData";
import {
  appointmentComponent,
  appointmentList,
} from "../../Styles/Appointments/AppointmentListStyles";
import { Appointment } from "./Appointment";

interface Props {
  data: AppointmentData[];
}

export const AppointmentList = ({ data }: Props) => {
  return (
    <ul css={appointmentList}>
      {data.map((appoint) => (
        <li key={appoint.id} css={appointmentComponent}>
          <Appointment appointment={appoint} />
        </li>
      ))}
    </ul>
  );
};
