/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useSearchParams } from "react-router-dom";
import { getDoctors } from "../Api/DoctorsData";
import {
  gettingDoctorsAction,
  gotDoctorsAction,
} from "../Store/ActionCreators/DoctorActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import { forSearch } from "../Styles/SearchPageStyles";
import { DoctorList } from "./Doctors/DoctorList";
import { Page } from "./General/Page";

export const SearchPage = () => {
  const [searchParams] = useSearchParams();
  const searchTerm = searchParams.get("fullNameSearchTerm") || "";
  const dispatch = useDispatch();
  const doctors = useSelector((state: AppState) => state.doctors.doctors);
  const doctorsLoading = useSelector(
    (state: AppState) => state.doctors.loading,
  );

  useEffect(() => {
    const doSearch = async (fullNameSearchTerm: string) => {
      dispatch(gettingDoctorsAction());
      const results = await getDoctors({
        fullNameSearchTerm: fullNameSearchTerm,
      });
      dispatch(gotDoctorsAction(results));
    };
    doSearch(searchTerm);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [searchTerm]);

  return (
    <Page title="Search Results">
      {searchTerm && <p css={forSearch}>for "{searchTerm}"</p>}
      {doctorsLoading ? <div>Loading...</div> : <DoctorList data={doctors} />}
    </Page>
  );
};
