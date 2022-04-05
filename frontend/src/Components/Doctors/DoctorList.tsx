/** @jsxImportSource @emotion/react */
import { DoctorData } from "../../Api/DoctorsData";
import { Doctor } from "./Doctor";
import {
  doctorComponent,
  doctorList,
} from "../../Styles/Doctors/DoctorListStyles";

interface Props {
  data: DoctorData[];
}

export const DoctorList = ({ data }: Props) => (
  <ul css={doctorList}>
    {data.map((doctor) => (
      <li key={doctor.id} css={doctorComponent}>
        <Doctor doctor={doctor} />
      </li>
    ))}
  </ul>
);
