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
      <div css={doctorSpecialzation}>Speciality: {doctor.specialization}</div>
      <div css={doctorExp}>Years of experience: {doctor.yearsOfExperience}</div>
    </Link>
  </div>
);
