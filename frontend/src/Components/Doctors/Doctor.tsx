/** @jsxImportSource @emotion/react */
import { Link } from "react-router-dom";
import { DoctorData } from "../../Api/DoctorsData";
import {
  doctorContainer,
  doctorExp,
  doctorSpecialzation,
  doctorTitle,
} from "../../Styles/Doctors/DoctorStyles";

interface Props {
  doctor: DoctorData;
}

export const Doctor = ({ doctor }: Props) => (
  <div css={doctorContainer}>
    <Link to={`/doctors/${doctor.id}`} css={doctorTitle}>
      {doctor.firstName} {doctor.lastName}
      <div css={doctorSpecialzation}>
        Специальность: {doctor.specialization}
      </div>
      <div css={doctorExp}>Опыт: {doctor.yearsOfExperience} лет</div>
    </Link>
  </div>
);
