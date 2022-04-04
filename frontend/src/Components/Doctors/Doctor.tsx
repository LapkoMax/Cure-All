/** @jsxImportSource @emotion/react */
import { DoctorData } from "../../Api/DoctorsData";
import {
  doctorContainer,
  doctorExp,
  doctorSpeciality,
  doctorTitle,
} from "../../Styles/Doctors/DoctorStyles";

interface Props {
  doctor: DoctorData;
}

export const Doctor = ({ doctor }: Props) => (
  <div css={doctorContainer}>
    <div css={doctorTitle}>
      {doctor.firstName} {doctor.lastName}
    </div>
    <div css={doctorSpeciality}>Speciality: {doctor.speciality}</div>
    <div css={doctorExp}>Years of experience: {doctor.yearsOfExperience}</div>
  </div>
);
