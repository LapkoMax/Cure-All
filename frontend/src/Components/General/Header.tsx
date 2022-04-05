/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../Store/Reducers/RootReducer";
import {
  headerContainer,
  helloUserLabel,
  searchInput,
  signInAnchor,
  titleAnchor,
} from "../../Styles/General/HeaderStyles";
import { UserIcon } from "./Icons";

type FormData = {
  searchTerm: string;
};

export const Header = () => {
  const dispatch = useDispatch();
  const user = useSelector((state: AppState) => state.identity.user);
  const { register, handleSubmit } = useForm<FormData>();
  const [searchParams] = useSearchParams();
  const searchTerm = searchParams.get("fullNameSearchTerm") || "";
  const navigate = useNavigate();

  useEffect(() => {
    if (user === null && !window.location.href.toString().includes("signin")) {
      navigate("signin");
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [window.location.href, user]);

  const submitForm = ({ searchTerm }: FormData) => {
    searchTerm = searchTerm.trim();
    if (searchTerm !== "") navigate("search?fullNameSearchTerm=" + searchTerm);
  };

  return (
    <div css={headerContainer} className="d-flex justify-content-around">
      <Link
        to="/"
        css={titleAnchor}
        className="col-3 row d-flex justify-content-start"
      >
        Cure-All
      </Link>
      <form
        className="col-3 row d-flex justify-content-center"
        onSubmit={handleSubmit(submitForm)}
      >
        <input
          {...register("searchTerm")}
          type="text"
          placeholder="Search for doctor..."
          defaultValue={searchTerm}
          css={searchInput}
        />
      </form>
      {user ? (
        <div className="col-3 row d-flex justify-content-end">
          <div
            css={helloUserLabel}
            className="col-4 row d-flex justify-content-end"
          >
            Hello {user.userName}!
          </div>
          <Link
            to=""
            onClick={() => {
              dispatch(signOutUserAction());
            }}
            css={signInAnchor}
            className="col-4 row d-flex justify-content-end"
          >
            <div className="col-1">
              <UserIcon />
            </div>
            <span className="col-8 row">Sign Out</span>
          </Link>
        </div>
      ) : (
        <div className="col-3 row d-flex justify-content-end">
          <Link
            to="signin"
            css={signInAnchor}
            className="d-flex justify-content-end"
          >
            <UserIcon />
            <span>Sign In</span>
          </Link>
        </div>
      )}
    </div>
  );
};
