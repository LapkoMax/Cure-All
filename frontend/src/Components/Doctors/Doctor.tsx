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
  doctor?: DoctorData | null;
}

export const Doctor = ({ doctor }: Props) => (
  <div css={doctorContainer}>
    <Link to={`/profile/${doctor?.userId}`} css={doctorTitle}>
      {doctor?.firstName} {doctor?.lastName}
      <div css={doctorSpecialzation}>
        Специальность: {doctor?.specialization}
      </div>
      <div css={doctorExp}>Опыт: {doctor?.yearsOfExperience} лет</div>
    </Link>
  </div>
);
